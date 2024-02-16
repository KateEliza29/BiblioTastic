import { Component } from '@angular/core';
import { faPenToSquare } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { LibraryContentsService } from '../../Services/library-contents.service';
import { Book } from '../../Models/Book.Model';
import { CommonModule } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { JournalArticle } from '../../Models/JournalArticle.Model';
import { StarRatingComponent } from '../../Shared/star-rating/star-rating.component';

@Component({
  selector: 'app-details',
  standalone: true,
  imports: [FontAwesomeModule, CommonModule, StarRatingComponent],
  templateUrl: './details.component.html',
  styleUrl: './details.component.css'
})
export class DetailsComponent {
  public book!: Book | null;
  public journalArticle!: JournalArticle | null;
  faPenToSquare = faPenToSquare;

  constructor(private libraryContentsService: LibraryContentsService, public dialog: MatDialog) {}

  ngOnInit(): void {
    this.libraryContentsService.selectedBookChanged.subscribe(result => {
      this.book = result;
      this.journalArticle = null;
    });

    this.libraryContentsService.selectedJournalArticleChanged.subscribe(result => {
      this.journalArticle = result;
      this.book = null;
    });
  }
}
