import { AdditionalInfo } from "./AdditionalInfo.Model";

export interface JournalArticle {
    journalArticleID: number;
    libraryID: number;
    journalTitle: string;
    articleTitle: string;
    author: string;
    volumeNumber: string;
    issueNumber: string;
    pageReference: string;
    url: string;
    publicationYear: number;
    dateAdded: Date;
    harvardReference: string;
    additionalInfo: AdditionalInfo;
}