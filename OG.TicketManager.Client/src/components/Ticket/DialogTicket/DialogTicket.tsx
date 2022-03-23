import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  TextField,
} from "@mui/material";
import React from "react";
import { DialogTicketStyles } from "./DialogTicket.styles";
import { useFormik } from "formik";
import * as yup from "yup";
import { TicketService } from "../../../services/TicketService";
import { SweetAlertService } from "../../../services/SweetAlertService";

interface DialogTicketProps {
  open: boolean;
  onClose: () => void;
}

export const DialogTicket = (props: DialogTicketProps) => {
  const styles = DialogTicketStyles;

  const { onClose, open } = props;
  const validations = yup.object({
    description: yup.string().required("Description is required"),
  });
  const formik = useFormik<CreateTicketCommand>({
    initialValues: { description: "" },
    validationSchema: validations,
    onSubmit: (value) => handleSubmit(value),
  });

  const handleSubmit = (ticket: CreateTicketCommand) => {
    TicketService.createTicket(ticket)
      .then((response) => {
        SweetAlertService.AlertSuccess(
          "Success",
          `Ticket #${response.data} created successfully`
        );
        onClose();
      })
      .catch((ex) => console.error(ex));
  };

  return (
    <Dialog fullWidth maxWidth="sm" open={open}>
      <DialogTitle>New ticket</DialogTitle>
      <DialogContent>
        <DialogContentText>Type ticket description...</DialogContentText>
        <DialogTicketStyles.Form>
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
        </DialogTicketStyles.Form>
      </DialogContent>
      <DialogActions>
        <Button
          onClick={() => formik.handleSubmit()}
          disabled={!formik.isValid}
        >
          Save
        </Button>
        <Button onClick={onClose}>Close</Button>
      </DialogActions>
    </Dialog>
  );
};
