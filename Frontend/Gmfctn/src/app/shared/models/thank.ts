import { UserSI } from './user-short-info';

export interface Thank {
    toUserId: string;
    fromUser: UserSI;
    text: string;
    addedTime: string;
}
