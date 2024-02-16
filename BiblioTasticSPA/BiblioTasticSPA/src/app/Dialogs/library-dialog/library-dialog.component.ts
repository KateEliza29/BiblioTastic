import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Library } from '../../Models/Library.Model';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
import { faFloppyDisk } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { LibraryService } from '../../Services/library.service';

@Component({
  selector: 'app-library-dialog',
  standalone: true,
  imports: [CommonModule, MatInputModule, MatFormFieldModule, FormsModule, FontAwesomeModule],
  templateUrl: './library-dialog.component.html',
  styleUrl: './library-dialog.component.css'
})
export class LibraryDialogComponent {
  faFloppyDisk = faFloppyDisk;
  public saveMessage!: string;

  constructor(@Inject(MAT_DIALOG_DATA) public data: Library, private libraryService: LibraryService) {}

  public saveLibrary() {
    if (this.canSave()) {
      this.libraryService.SaveLibrary(this.data).subscribe(result => {
        if (result > 0)
          this.saveMessage = "Your library has been saved.";
        else 
          this.saveMessage = "Error. Please try again later.";
      });
    }
    else {
      this.saveMessage = "Please make sure you have filled in all the fields.";
    }
  }

  private canSave(): boolean{
    var canSave = false;
    if (this.data.libraryName.length > 1 && this.data.libraryDescription.length > 1)
      canSave = true;

    return canSave;
  }
}
