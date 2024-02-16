import { Component, OnInit, inject } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { LibraryService } from '../../Services/library.service';
import { Library } from '../../Models/Library.Model';
import { CommonModule } from '@angular/common';
import { DetailCardComponent } from '../detail-card/detail-card.component';
import { faCirclePlus } from '@fortawesome/free-solid-svg-icons';
import { LibraryDialogComponent } from '../../Dialogs/library-dialog/library-dialog.component';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-libraries',
  standalone: true,
  imports: [FontAwesomeModule, CommonModule, DetailCardComponent, MatDialogModule, LibraryDialogComponent],
  templateUrl: './libraries.component.html',
  styleUrl: './libraries.component.css'
})
export class LibrariesComponent implements OnInit {
  public libraries! : Library[];

  faCirclePlus = faCirclePlus;

  constructor(private libraryService: LibraryService, public dialog: MatDialog) {  }

  ngOnInit(): void {
    this.libraryService.GetLibraries().subscribe(result => {
      this.libraries = result;
    })
  }

  public libraryClick(library: Library) {
    this.libraryService.SetLibrary(library);
  }

  public addNewLibrary() {
    let dialogRef = this.dialog.open(LibraryDialogComponent, {
      data: {
        libraryID: 0,
        libraryName: "",
        libraryDescription: ""
      }
    }); 
  }
}

