import { Component, OnInit } from '@angular/core';
import { faBook } from '@fortawesome/free-solid-svg-icons';
import { faNewspaper } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { LibraryContentsService } from '../../Services/library-contents.service';
import { Book } from '../../Models/Book.Model';
import { LibraryService } from '../../Services/library.service';
import { DetailCardComponent } from '../detail-card/detail-card.component';
import { CommonModule } from '@angular/common';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { BookDialogComponent } from '../../Dialogs/book-dialog/book-dialog.component';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { FormsModule } from '@angular/forms';
import { JournalArticle } from '../../Models/JournalArticle.Model';
import { JournalArticleDialogComponent } from '../../Dialogs/journal-article-dialog/journal-article-dialog.component';

@Component({
  selector: 'app-library-contents',
  standalone: true,
  imports: [FontAwesomeModule, DetailCardComponent, CommonModule,  MatDialogModule, BookDialogComponent,  FormsModule, MatButtonToggleModule],
  templateUrl: './library-contents.component.html',
  styleUrl: './library-contents.component.css'
})
export class LibraryContentsComponent implements OnInit {
  public books!: Book[];
  public journalArticles!: JournalArticle[];
  public contentsType: string = 'books';

  //Icons
  faBook = faBook;
  faNewspaper = faNewspaper;

  constructor(private libraryContentsService: LibraryContentsService, private libraryService: LibraryService, public dialog: MatDialog) {}

  ngOnInit(): void {
    //Pull in the books and journal articles to be displayed for the chosen library.
    this.libraryService.selectedLibraryChanged.subscribe(result => {
      this.libraryContentsService.GetBooks(this.libraryService.selectedLibrary.libraryID).subscribe(result => {
        this.books = result;
      });

      this.libraryContentsService.GetJournalArticles(this.libraryService.selectedLibrary.libraryID).subscribe(result => {
        this.journalArticles = result;
      })
    });
  }

  public addNewBook() {
    let additionalInfo = {
      additionalInfoID: 0,
      resourceType: 1,
      resourceID: 0,
      summary: "",
      keyWords: "",
      rating: 0,
    }
    let dialogRef = this.dialog.open(BookDialogComponent, {
      data: {
        bookID: 0,
        libraryID: this.libraryService.selectedLibrary.libraryID,
        title: "",
        author: "",
        publicationYear: 0,
        URL: "",
        publicationLocation: "",
        publisher: "",
        date: new Date(),
        additionalInfo: additionalInfo
      }
    }); 
    dialogRef.afterClosed().subscribe(result => {
      this.libraryContentsService.GetBooks(this.libraryService.selectedLibrary.libraryID).subscribe(result => {
        this.books = result;
      })
    });
  }


  public addNewJournalArticle() {
    let additionalInfo = {
      additionalInfoID: 0,
      resourceType: 2,
      resourceID: 0,
      summary: "",
      keyWords: "",
      rating: 0,
    }
    let dialogRef = this.dialog.open(JournalArticleDialogComponent, {
      data: {
        journalArticleID: 0,
        libraryID: this.libraryService.selectedLibrary.libraryID,
        journalTitle: "",
        articleTitle: "",
        author: "",
        volumeNumber: "",
        issueNumber: "",
        pageReference: "",
        URL: "",
        publicationDate: 0,
        dateAdded: new Date,
        additionalInfo: additionalInfo
      }
    }); 
    dialogRef.afterClosed().subscribe(result => {
      this.libraryContentsService.GetJournalArticles(this.libraryService.selectedLibrary.libraryID).subscribe(result => {
        this.journalArticles = result;
      })
    });
  }
}
