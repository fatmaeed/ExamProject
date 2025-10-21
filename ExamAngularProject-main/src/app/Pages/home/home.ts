import { Component } from '@angular/core';
import { HeroSection } from "../../componantsRahma/hero-section/hero-section";
import { HowToPlayComponent } from "../../componantsRahma/how-to-play/how-to-play";
import { Quizzes } from "../../componantsRahma/quizzes/quizzes";
import { NavBar } from "../../layout/nav-bar/nav-bar";
import { Footer } from "../../layout/footer/footer";

@Component({
  selector: 'app-home',
  imports: [HeroSection, HowToPlayComponent, HowToPlayComponent, Quizzes, NavBar, Footer],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home {

}
