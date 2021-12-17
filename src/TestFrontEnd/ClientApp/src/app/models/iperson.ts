export interface IPerson {
    firstName: string;
    lastName: string;
    date: Date;
}

export interface IBaseResponse<T> {
    succes: boolean;
    value: T;
}
