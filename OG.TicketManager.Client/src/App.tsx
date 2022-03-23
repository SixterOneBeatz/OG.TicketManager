import { ThemeProvider } from "@mui/material/styles";
import React, { useState } from "react";
import { MainToolbar } from "./components/layout/MainToolbar/MainToolbar";
import { DialogTicket } from "./components/Ticket/DialogTicket/DialogTicket";
import { DialogUpdate } from "./components/Ticket/DialogUpdate/DialogUpdate";
import { TableTickets } from "./components/Ticket/TableTickets/TableTickets";
import { Ticket } from "./models/Tickets/Tickets";
import MainTheme from "./shared/MainTheme";

const App = () => {
  const [openDialogCreate, setOpenDialogCreate] = useState(false);
  const handleCreateNew = () => setOpenDialogCreate(true);
  const handleCloseDialogCreate = () => setOpenDialogCreate(false);

  const [ticketToUpdate, setTicketToUpdate] = useState<Ticket>();
  const handleEdit = (ticket: Ticket) => {
    setTicketToUpdate(ticket);
  };

  return (
    <ThemeProvider theme={MainTheme}>
      <MainToolbar />
      <TableTickets
        onCreateNewTicket={handleCreateNew}
        onEditTicket={(ticket) => handleEdit(ticket)}
      />
      <DialogTicket open={openDialogCreate} onClose={handleCloseDialogCreate} />
      <DialogUpdate ticket={ticketToUpdate} />
    </ThemeProvider>
  );
};
export default App;
