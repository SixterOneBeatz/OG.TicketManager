import * as signalR from "@microsoft/signalr";

const hubURL = process.env.HUB_URL;

export let hubConnection: signalR.HubConnection;

export const startConnection = () => {
  hubConnection = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.None)
    .withUrl(`${hubURL}`)
    .withAutomaticReconnect()
    .build();

  hubConnection
    .start()
    .then(() => console.log("SignalR Connected"))
    .catch((ex) => {
      console.error(ex);
    });
};