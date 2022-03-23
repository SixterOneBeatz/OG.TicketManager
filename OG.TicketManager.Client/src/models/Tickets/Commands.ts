interface CreateTicketCommand {
  description: string;
}

interface ResolveTicketCommand {
  id: number;
}

interface UpdateTicketCommand {
  id: number;
  description: string;
  historyDescription: string;
}
