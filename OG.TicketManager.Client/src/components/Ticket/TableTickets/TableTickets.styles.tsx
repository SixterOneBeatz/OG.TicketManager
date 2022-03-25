import { Box, Paper, Toolbar } from "@mui/material";
import styled from "styled-components";

const StyledPaper = styled(Paper)`
  width: 100%;
  overflow: hidden;
`;
const StyledBox = styled(Box)`
  position: fixed;
  left: 0;
  bottom: 0;
  width: 100%;
  text-align: center;
  flex-grow: 1;
`;

const FooterToolbar = styled(Toolbar)`
  justify-content: space-between;
  background-color: whitesmoke;
`;

export const TableTicketsStyles = {
  StyledBox,
  FooterToolbar,
  StyledPaper,
};
