import { Component } from '@angular/core';
import { LibrariesComponent } from '../../Library/libraries/libraries.component';
import { LibraryContentsComponent } from '../../Library/library-contents/library-contents.component';
import { DetailsComponent } from '../../Library/details/details.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [LibrariesComponent, LibraryContentsComponent, DetailsComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

}
