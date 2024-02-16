import { AdditionalInfo } from './AdditionalInfo.Model';

export interface Book {
    bookID: number;
    libraryID: number;
    title: string;
    author: string;
    publicationYear: number;
    url: string;
    publicationLocation: string;
    publisher: string;
    dateAdded: Date;
    harvardReference: string;
    additionalInfo: AdditionalInfo;
}