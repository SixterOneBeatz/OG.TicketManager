import React, { useEffect, useState } from "react";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TablePagination from "@mui/material/TablePagination";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import IconButton from "@mui/material/IconButton";
import LibraryAddCheckIcon from "@mui/icons-material/LibraryAddCheck";
import DeleteForeverIcon from "@mui/icons-material/DeleteForever";
import {
  AppBar,
  Box,
  Button,
  TableHead,
  Toolbar,
  Tooltip,
} from "@mui/material";
import { Ticket } from "../../../models/Tickets/Tickets";
import { TableTicketsStyles } from "./TableTickets.styles";
import { Column } from "../../../shared/Models";
import { Add, Edit } from "@mui/icons-material";
import { TicketService } from "../../../services/TicketService";
import {
  AlertConfirm,
  AlertSuccess,
} from "../../../services/SweetAlertService";
import {
  startConnection,
  hubConnection,
} from "../../../services/SignalRService";

interface TicketsTableColumn extends Column {
  id: "id" | "createdDate" | "description" | "lastHistory" | "dateResolved";
  isDate?: boolean;
}

const columns: TicketsTableColumn[] = [
  { id: "id", label: "#Ticket", align: "center", width: 10 },
  {
    id: "createdDate",
    label: "Created at",
    align: "center",
    width: 15,
    isDate: true,
  },
  { id: "description", label: "Description", align: "center", width: 20 },
  { id: "lastHistory", label: "Last History", align: "center", width: 15 },
  {
    id: "dateResolved",
    label: "Resolved at",
    align: "center",
    width: 20,
    isDate: true,
  },
];

interface TableTicketsProps {
  onCreateNewTicket: () => void;
  onEditTicket: (ticket: Ticket) => void;
}

export const TableTickets = (props: TableTicketsProps) => {
  const styles = TableTicketsStyles;

  const { onCreateNewTicket, onEditTicket } = props;
  const [tickets, setTickets] = useState<Ticket[]>([]);
  const [page, setPage] = useState(0);
  const [rowsPerPAge, setRowsPerPage] = useState(10);

  const handleChangePage = (event: unknown, newPage: number) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setRowsPerPage(+event.target.value);
    setPage(0);
  };

  const getTickets = () =>
    TicketService.getTickets()
      .then((response) => setTickets([...response.data]))
      .catch((ex) => {
        setTickets([]);
        console.error(ex);
      });

  const deleteTicket = (id: number) =>
    AlertConfirm("Confirm", `Are you sure to delete ticket #${id}?`, () => {
      TicketService.deleteTicket(id)
        .then((response) =>
          AlertSuccess(
            "Success",
            `Ticket #${response.data} has been deleted successfully`
          )
        )
        .catch((ex) => console.error(ex));
    });

  const resolveTicket = (ticketCommand: ResolveTicketCommand) =>
    AlertConfirm(
      "Confirm",
      `Are you sure to resolve ticket #${ticketCommand.id}?`,
      () => {
        TicketService.resolveTicket(ticketCommand)
          .then((response) =>
            AlertSuccess(
              "Success",
              `Ticket #${response.data} has been resolved successfully`
            )
          )
          .catch((ex) => console.error(ex));
      }
    );

  useEffect(() => {
    getTickets();
    startConnection();
    if (hubConnection) {
      hubConnection.on("sendTickets", (data: Ticket[]) => {
        setTickets(data);
      });
    }
  }, []);

  return (
    <Paper sx={styles.Paper}>
      <TableContainer>
        <Table stickyHeader>
          <TableHead>
            <TableRow>
              {columns.map((c) => (
                <TableCell key={c.id} align={c.align} sx={{ width: c.width }}>
                  {c.label}
                </TableCell>
              ))}
              <TableCell align={"center"} sx={{ width: 20 }}>
                Actions
              </TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {tickets
              .slice(page * rowsPerPAge, page * rowsPerPAge + rowsPerPAge)
              .map((r) => (
                <TableRow hover tabIndex={-1} key={r.id}>
                  {columns.map((c) => {
                    const value = r[c.id];
                    return (
                      <TableCell key={c.id} align={c.align}>
                        {c.isDate
                          ? value !== null
                            ? new Date(value as string).toLocaleString()
                            : "Not resolved yet"
                          : value}
                      </TableCell>
                    );
                  })}
                  <TableCell align={"center"} sx={{ width: 20 }}>
                    <IconButton
                      color="primary"
                      component="span"
                      disabled={r.isDeleted || r.dateResolved !== null}
                      onClick={() => onEditTicket(r)}
                    >
                      <Edit />
                    </IconButton>
                    <IconButton
                      color="secondary"
                      component="span"
                      disabled={r.isDeleted || r.dateResolved !== null}
                      onClick={() => resolveTicket({ id: r.id })}
                    >
                      <LibraryAddCheckIcon />
                    </IconButton>
                    <IconButton
                      color="error"
                      component="span"
                      disabled={r.isDeleted || r.dateResolved !== null}
                      onClick={() => deleteTicket(r.id)}
                    >
                      <DeleteForeverIcon />
                    </IconButton>
                  </TableCell>
                </TableRow>
              ))}
          </TableBody>
        </Table>
      </TableContainer>
      <Box sx={styles.Footer}>
        <AppBar position="static">
          <Toolbar sx={styles.FooterToolbar}>
            <span>
              <Tooltip title="Add ticket" arrow placement="left-start">
                <Button
                  variant="contained"
                  color="primary"
                  size="large"
                  onClick={onCreateNewTicket}
                >
                  <Add />
                </Button>
              </Tooltip>
            </span>
            <TablePagination
              component="div"
              count={tickets.length}
              rowsPerPage={rowsPerPAge}
              page={page}
              onPageChange={handleChangePage}
              onRowsPerPageChange={handleChangeRowsPerPage}
              rowsPerPageOptions={[
                10,
                25,
                50,
                { value: tickets.length, label: "All" },
              ]}
            />
          </Toolbar>
        </AppBar>
      </Box>
    </Paper>
  );
};
