export interface Ticket {
  id: number;
  createdDate?: Date;
  description: string;
  lastHistory: string;
  dateResolved?: Date;
  isDeleted: boolean;
}

export interface History {
  createdDate: Date;
  description: string;
}
