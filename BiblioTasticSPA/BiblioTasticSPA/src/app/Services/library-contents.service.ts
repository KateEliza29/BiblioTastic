import { EventEmitter, Injectable, Output } from '@angular/core';
import { Book } from '../Models/Book.Model';
import { HttpClient } from '@angular/common/http';
import { JournalArticle } from '../Models/JournalArticle.Model';

@Injectable({
  providedIn: 'root'
})
export class LibraryContentsService {
  //Book Stuff
  private bookURL = 'connstring';
  public SelectedBook! : Book;
  @Output() selectedBookChanged: EventEmitter<Book> = new EventEmitter();

  //Journal Article Stuff
  private journalArticleURL = 'connstring';
  public SelectedJournalArticle! : JournalArticle;
  @Output() selectedJournalArticleChanged: EventEmitter<JournalArticle> = new EventEmitter();

  constructor(private http: HttpClient) { }

  //Books 
  public GetBooks(libraryID: number) {
    return this.http.get<Book[]>(this.bookURL + "/" + libraryID);
  }

  public SetBook(book: Book) {
    this.SelectedBook = book;
    this.selectedBookChanged.emit(book);
  }

  public SaveBook(book: Book) {
    return this.http.post<number>(this.bookURL, book)
  }

  //Journal Articles
  public GetJournalArticles(libraryID: number) {
    return this.http.get<JournalArticle[]>(this.journalArticleURL + "/" + libraryID);
  }

  public SetJournalArticle(journalArticle: JournalArticle) {
    this.SelectedJournalArticle = journalArticle;
    this.selectedJournalArticleChanged.emit(journalArticle);
  }

  public SaveJournalArticle(journalArticle: JournalArticle) {
    return this.http.post<number>(this.journalArticleURL, journalArticle)
  }
}
