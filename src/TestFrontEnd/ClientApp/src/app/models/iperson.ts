export interface IPerson {
    id: number;
    firstName: string;
    lastName: string;
    date: Date;
    number: number;
}

export interface IBaseResponse<T> {
    succes: boolean;
    value: T;
}
