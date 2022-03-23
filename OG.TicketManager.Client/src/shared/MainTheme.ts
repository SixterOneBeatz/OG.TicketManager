import { createTheme } from "@mui/material/styles";
import "@fontsource/poppins";

const MainTheme = createTheme({
  palette: {
    primary: {
      main: "#293241",
    },
    secondary: {
      main: "#ee6c4d",
    },
    error: {
      main: "#a4161a",
    },
    warning: {
      main: "#fb5607",
    },
    info: {
      main: "#98c1d9",
    },
    success: {
      main: "#8cb369",
    },
  },
  typography: {
    fontFamily: "Poppins",
  },
});

export default MainTheme;
