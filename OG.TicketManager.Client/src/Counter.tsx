import { Button } from "@mui/material";
import React, { useEffect } from "react";

const Counter = () => {
  let apiUrl = process.env.API_URL;

  useEffect(() => {
    console.log(apiUrl);
  }, []);

  const [count, setCount] = React.useState<number>(0);

  const increment = () => setCount((count) => count + 1);

  const decrement = () => setCount((count) => count - 1);

  const incrementBy10 = () => setCount((count) => count + 10);

  const decrementBy10 = () => setCount((count) => count - 10);

  return (
    <div>
      <h2>
        Number: <b>{count}</b>
      </h2>
      <br />
      <br />
      <Button variant="contained" onClick={() => increment()}>Increment</Button>{" "}
      <Button variant="contained" onClick={() => decrement()}>Decrement</Button>{" "}
      <Button variant="contained" onClick={() => incrementBy10()}>Increment 10</Button>{" "}
      <Button variant="contained" onClick={() => decrementBy10()}>Decrement 10</Button>{" "}
    </div>
  );
};

export default Counter;
