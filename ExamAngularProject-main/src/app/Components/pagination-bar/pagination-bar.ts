import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-pagination-bar',
  imports: [],
  templateUrl: './pagination-bar.html',
  styleUrl: './pagination-bar.css'
})
export class PaginationBar {
  @Output() pageChange = new EventEmitter();
  @Input({required : true}) pages!: number[];
 @Input({required : true}) selectedPage !: number  ;
  changePage(page : number){
    if(this.selectedPage === page) return ;
    this.selectedPage = page;
    this.pageChange.emit(this.selectedPage);

  }
  nextPage(){
    if(this.selectedPage === this.pages.length) return ;
    this.selectedPage ++ ;
    this.pageChange.emit(this.selectedPage);

  }
  prevPage(){
    if(this.selectedPage === 1) return ;
    this.selectedPage -- ;
    this.pageChange.emit(this.selectedPage);

  }

}
