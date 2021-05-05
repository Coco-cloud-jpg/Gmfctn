import { UserSI } from './user-short-info';

export interface EventIS {
    description: string;
    createdTime: string;
    type: EventType;
    userId: string;
    user: UserSI;
    date: Date;
}

enum EventType {
    Race = 'Race',
    Records = 'Records',
    Challenge = 'Challenge',
    Upload = 'Upload'
}
