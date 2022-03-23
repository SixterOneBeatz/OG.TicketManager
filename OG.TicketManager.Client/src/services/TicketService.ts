import { Ticket } from "../models/Tickets/Tickets";
import client from "./HttpClient";

const ENDPOINT = "/Ticket";

const getTickets = () => client.get<Ticket[]>(`${ENDPOINT}/GetTickets`);

const getTicket = (id: number) =>
  client.get<Ticket>(`${ENDPOINT}/GetTickets/${id}`);

const createTicket = (ticketCommand: CreateTicketCommand) =>
  client.post<number>(`${ENDPOINT}/CreateTicket`, ticketCommand);

const resolveTicket = (ticketCommand: ResolveTicketCommand) =>
  client.put<number>(`${ENDPOINT}/ResolveTicket`, ticketCommand);

const updateTicket = (ticketCommand: UpdateTicketCommand) =>
  client.put<number>(`${ENDPOINT}/UpdateTicket`, ticketCommand);

const deleteTicket = (id: number) =>
  client.delete(`${ENDPOINT}/DeleteTicket/${id}`);

export const TicketService = {
  getTicket,
  getTickets,
  createTicket,
  resolveTicket,
  updateTicket,
  deleteTicket,
};
