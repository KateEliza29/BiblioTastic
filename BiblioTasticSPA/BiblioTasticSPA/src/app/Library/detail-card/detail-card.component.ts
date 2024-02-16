import { Component, Input } from '@angular/core';
import { Library } from '../../Models/Library.Model';
import { CommonModule } from '@angular/common';
import { Book } from '../../Models/Book.Model';
import { LibraryService } from '../../Services/library.service';
import { LibraryContentsService } from '../../Services/library-contents.service';
import { faPenToSquare } from '@fortawesome/free-solid-svg-icons';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { LibraryDialogComponent } from '../../Dialogs/library-dialog/library-dialog.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { BookDialogComponent } from '../../Dialogs/book-dialog/book-dialog.component';
import { JournalArticle } from '../../Models/JournalArticle.Model';
import { JournalArticleDialogComponent } from '../../Dialogs/journal-article-dialog/journal-article-dialog.component';

@Component({
  selector: 'app-detail-card',
  standalone: true,
  imports: [CommonModule, MatDialogModule, LibraryDialogComponent, FontAwesomeModule],
  templateUrl: './detail-card.component.html',
  styleUrl: './detail-card.component.css'
})

export class DetailCardComponent {
 @Input() type! : string;
 @Input() library! : Library;
 @Input() book!: Book;
 @Input() journalArticle!: JournalArticle;

 //Icons
 faPenToSquare = faPenToSquare;

 constructor(private libraryService: LibraryService, private libraryContentsService: LibraryContentsService, public dialog: MatDialog) {}

  public libraryClick(library: Library) {
    this.libraryService.SetLibrary(library);
  }

  public bookClick(book: Book) {
    this.libraryContentsService.SetBook(book);
  }

  public journalArticleClick(journalArticle: JournalArticle) {
    this.libraryContentsService.SetJournalArticle(journalArticle);
  }

  public editLibrary(library: Library) {
    let dialogRef = this.dialog.open(LibraryDialogComponent, {
      data: library
    }); 
  }

  public editBook(book: Book) {
    let dialogRef = this.dialog.open(BookDialogComponent, {
      data: book
    }); 
  }

  public editJournalArticle(journalArticle: JournalArticle) {
    let dialogRef = this.dialog.open(JournalArticleDialogComponent, {
      data: journalArticle
    }); 
  }

}
