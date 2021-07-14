export interface IPerson {
    firstName: string;
    lastName: string;
}

export interface IBaseResponse<T> {
    succes: boolean;
    value: T;
}
