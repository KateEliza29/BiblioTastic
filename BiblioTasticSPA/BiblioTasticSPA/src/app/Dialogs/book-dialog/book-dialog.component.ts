import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Book } from '../../Models/Book.Model';
import { LibraryContentsService } from '../../Services/library-contents.service';
import { faFloppyDisk } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-book-dialog',
  standalone: true,
  imports: [FontAwesomeModule, MatInputModule, MatFormFieldModule, FormsModule, CommonModule],
  templateUrl: './book-dialog.component.html',
  styleUrl: './book-dialog.component.css'
})
export class BookDialogComponent {
  faFloppyDisk = faFloppyDisk;
  public saveMessage!: string;

  constructor(@Inject(MAT_DIALOG_DATA) public data: Book, private libraryContentsService: LibraryContentsService) {}

  public saveBook() {
    if (this.canSave()) {
      this.libraryContentsService.SaveBook(this.data).subscribe(result => {
        if (result > 0)
          this.saveMessage = "Your book has been saved.";
        else 
          this.saveMessage = "Error. Please try again later.";
      });
    }
    else {
      this.saveMessage = "Please make sure that you have filled in the 'Title' and 'Author' fields."
    }
  }

  private canSave(): boolean{
    var canSave = false;
    let ratingCorrectFormat = this.data.additionalInfo.rating >=0 && this.data.additionalInfo.rating < 6;
    let publicationYearCorrectForm = this.data.publicationYear > 1800 && this.data.publicationYear < 2026;
    if (this.data.title.length > 1 && this.data.author.length > 1 && ratingCorrectFormat)
      canSave = true;

    return canSave;
  }
}
