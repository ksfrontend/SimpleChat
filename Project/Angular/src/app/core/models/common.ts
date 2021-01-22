export class LoggedInUserModel {
    constructor(
        public UserId: number,
        public UserName: string,
        public Token: string
    ) {}
}

export class ServiceResponse {
    IsSuccess: boolean;
    Message: string;
    Data: object;
    ErrorCode: string;
}

export class ChatMessage {
    Message: string;
}

export class SendChatMessageModel {
    public SenderId: number;
    public ReceiverId: number;
    public TextMessage: string;
}
