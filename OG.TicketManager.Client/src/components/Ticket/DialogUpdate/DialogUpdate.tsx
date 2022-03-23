import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  Grid,
  TextField,
} from "@mui/material";
import React, { useEffect, useState } from "react";
import { Ticket } from "../../../models/Tickets/Tickets";
import { useFormik } from "formik";
import * as yup from "yup";
import { DialogUpdateStyles } from "./DialogUpdate.styles";
import { TicketService } from "../../../services/TicketService";
import { AlertSuccess } from "../../../services/SweetAlertService";

interface DialogUpdateProps {
  ticket: Ticket | undefined;
}

export const DialogUpdate = (props: DialogUpdateProps) => {
  const { ticket } = props;
  const [open, setOpen] = useState(false);
  const styles = DialogUpdateStyles;
  const validations = yup.object({
    id: yup.number().min(1, "Invalid ticket Id"),
    description: yup.string().required("Not empty description"),
    historyDescription: yup.string().required("Not empty history description"),
  });

  const formik = useFormik<UpdateTicketCommand>({
    initialValues: { id: 0, description: "", historyDescription: "" },
    validationSchema: validations,
    onSubmit: (value) => handleSubmit(value),
  });

  const handleSubmit = (ticketCommand: UpdateTicketCommand) => {
    TicketService.updateTicket(ticketCommand)
      .then((response) => {
        AlertSuccess(
          "Success",
          `Ticket #${response.data} has been updated successfully`
        );
        setOpen(false);
      })
      .catch((ex) => console.error(ex));
  };

  useEffect(() => {
    if (ticket) {
      formik.setValues({
        id: ticket.id,
        description: ticket.description,
        historyDescription: "",
      });
      setOpen(true);
    }
  }, [ticket]);

  return (
    <Dialog fullWidth maxWidth="sm" open={open}>
      <DialogTitle>Update ticket</DialogTitle>
      <DialogContent>
        <DialogContentText>Type ticket information...</DialogContentText>
        <form style={styles.Form}>
          <Grid container spacing={2}>
            <Grid item xs={12} md={6}>
              <TextField
                color="primary"
                name="description"
                variant="outlined"
                fullWidth
                label="Description"
                multiline
                minRows={2}
                maxRows={6}
                value={formik.values.description}
                onChange={formik.handleChange}
              />
            </Grid>
            <Grid item xs={12} md={6}>
              <TextField
                color="primary"
                name="historyDescription"
                variant="outlined"
                fullWidth
                label="History description"
                multiline
                minRows={2}
                maxRows={6}
                value={formik.values.historyDescription}
                onChange={formik.handleChange}
              />
            </Grid>
          </Grid>
        </form>
      </DialogContent>
      <DialogActions>
        <Button
          onClick={() => formik.handleSubmit()}
          disabled={!formik.isValid}
        >
          Save
        </Button>
        <Button onClick={() => setOpen(false)}>Close</Button>
      </DialogActions>
    </Dialog>
  );
};
