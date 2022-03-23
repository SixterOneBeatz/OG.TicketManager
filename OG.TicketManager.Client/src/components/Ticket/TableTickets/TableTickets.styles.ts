import { CSSProperties } from "@mui/styled-engine";

const Paper: CSSProperties = {
  width: "100%",
  overflow: "hidden",
};

const Footer: CSSProperties = {
  position: "fixed",
  left: 0,
  bottom: 0,
  width: "100%",
  textAlign: "center",
  flexGrow: 1,
};

const FooterToolbar: CSSProperties = {
  justifyContent: "space-between",
  backgroundColor: "whitesmoke",
};

export const TableTicketsStyles = {
  Footer,
  FooterToolbar,
  Paper,
};
