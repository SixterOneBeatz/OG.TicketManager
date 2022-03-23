import Swal from "sweetalert2";

export function AlertSuccess(title: string = "Success", msg: string = "") {
  Swal.fire({
    icon: "success",
    title: title,
    html: msg,
  });
}

export function AlertError(title: string = "Error", msg: string = "") {
  Swal.fire({
    icon: "error",
    title: title,
    html: msg,
  });
}

export function AlertWarning(title: string = "Warning", msg: string = "") {
  Swal.fire({
    icon: "warning",
    title: title,
    html: msg,
  });
}

export function AlertConfirm(
  title: string = "Confirm",
  msg: string = "",
  onConfirmFn = () => {},
  onDenyFn = () => {}
) {
  Swal.fire({
    icon: "question",
    title: title,
    html: msg,
    showCancelButton: true,
    confirmButtonColor: "#3085d6",
    cancelButtonColor: "#d33",
    confirmButtonText: "Confirm",
    cancelButtonText: "Cancel",
  }).then((result) => {
    if (result.isConfirmed) {
      onConfirmFn();
    } else {
      onDenyFn();
    }
  });
}

export const SweetAlertService = {
  AlertConfirm,
  AlertError,
  AlertSuccess,
  AlertWarning,
};
