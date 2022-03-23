import { AppBar, Box, Toolbar, Typography } from "@mui/material";
import React from "react";
import { AppBarStyles } from "./MainToolbar.styles";

export const MainToolbar = () => {
  return (
    <Box sx={AppBarStyles.Fx1}>
      <AppBar position="sticky">
        <Toolbar>
          <Typography variant="h6" component="div" sx={AppBarStyles.Fx1}>
            Ticket Manager
          </Typography>
        </Toolbar>
      </AppBar>
    </Box>
  );
};
