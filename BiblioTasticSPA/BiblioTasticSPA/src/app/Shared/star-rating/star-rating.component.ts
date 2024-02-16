import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faStar as farStar } from '@fortawesome/free-regular-svg-icons';
import { faStar as fasStar } from '@fortawesome/free-solid-svg-icons';
import { LibraryContentsService } from '../../Services/library-contents.service';

@Component({
  selector: 'app-star-rating',
  standalone: true,
  imports: [FontAwesomeModule, CommonModule],
  templateUrl: './star-rating.component.html',
  styleUrl: './star-rating.component.css'
})
export class StarRatingComponent implements OnInit {

  //Icons
  farStar = farStar;
  fasStar = fasStar;

  @Input() rating!: number;
  public emptyStars!: number;

  constructor(private libraryContentsService: LibraryContentsService) {}

  ngOnInit(): void {
    this.emptyStars = 5 - this.rating;

    this.libraryContentsService.selectedBookChanged.subscribe(result => {
      this.rating = result.additionalInfo.rating;
      this.emptyStars = 5 - this.rating;
    });
    this.libraryContentsService.selectedJournalArticleChanged.subscribe(result => {
      this.rating = result.additionalInfo.rating;
      this.emptyStars = 5 - this.rating;
    });
  }

}
