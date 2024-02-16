import { CommonModule } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faFloppyDisk } from '@fortawesome/free-solid-svg-icons';
import { LibraryContentsService } from '../../Services/library-contents.service';
import { JournalArticle } from '../../Models/JournalArticle.Model';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-journal-article-dialog',
  standalone: true,
  imports: [FontAwesomeModule, MatInputModule, MatFormFieldModule, FormsModule, CommonModule],
  templateUrl: './journal-article-dialog.component.html',
  styleUrl: './journal-article-dialog.component.css'
})
export class JournalArticleDialogComponent {
  faFloppyDisk = faFloppyDisk;
  public saveMessage!: string;

  constructor(@Inject(MAT_DIALOG_DATA) public data: JournalArticle, private libraryContentsService: LibraryContentsService) {}

  public saveJournalArticle() {
    if (this.canSave()) {
      this.libraryContentsService.SaveJournalArticle(this.data).subscribe(result => {
        if (result > 0)
          this.saveMessage = "Your journal article has been saved.";
        else 
          this.saveMessage = "Error. Please try again later.";
      });
    }
    else {
      this.saveMessage = "Please ensure that you have filled in the 'Journal Title', 'Article Title' and 'Author' fields.";
    }
  }

  private canSave(): boolean{
    var canSave = false;
    //These are the only three that need to be not null. 
    if (this.data.journalTitle.length > 1 && this.data.articleTitle.length > 1, this.data.author.length > 1)
      canSave = true;

    return canSave;
  }
}
