import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable, Output } from '@angular/core';
import { Library } from '../Models/Library.Model';

@Injectable({
  providedIn: 'root'
})
export class LibraryService {
  private libraryURL = 'connstring';
  public selectedLibrary! : Library;
  @Output() selectedLibraryChanged: EventEmitter<Library> = new EventEmitter();

  constructor(private http: HttpClient) { }


  public GetLibraries() {
    return this.http.get<Library[]>(this.libraryURL);
  }

  public SetLibrary(library: Library) {
    this.selectedLibrary = library;
    this.selectedLibraryChanged.emit(library);
  }

  public SaveLibrary(library: Library) {
    return this.http.post<number>(this.libraryURL, library)
  }
}