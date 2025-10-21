import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import {  debounceTime, distinctUntilChanged, filter, Subscription } from 'rxjs';

@Component({
  selector: 'app-search-bar',
  imports: [ReactiveFormsModule],
  templateUrl: './search-bar.html',
  styleUrl: './search-bar.css'
})
export class SearchBar implements OnDestroy , OnInit {
  searchTerm = new FormControl('');
   subscribe!: Subscription
  @Output() searchEvent = new EventEmitter<string | null>();

  constructor() { }
  ngOnInit(): void {
    this.subscribe =   this.searchTerm.valueChanges.pipe(
      debounceTime(1000),
      distinctUntilChanged(),
      filter((value) => value !== ' ')
    ).subscribe(
      (value) => {
        this.searchEvent.emit(value);
      }
  );
  }
  ngOnDestroy(): void {
    this.subscribe.unsubscribe();
  }
 
}
