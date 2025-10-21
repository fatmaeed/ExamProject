
import { Component } from '@angular/core';

@Component({
  selector: 'app-how-to-play',
  templateUrl: './how-to-play.html',
  styleUrl: './how-to-play.css'
})
export class HowToPlayComponent {
  steps = [
    {
      number: 1,
      image: 'assets/images/step1.png',
      description: 'Enter the Phone Number and Click Register'
    },
    {
      number: 2,
 image: 'assets/images/step2.png',
     description: 'Enter the Verification Code and click Verify.'
    },
    {
      number: 3,
 image: 'assets/images/step3.png',
   description: 'Enter your Info and click Play Quiz.'
    },
    {
      number: 4,
 image: 'assets/images/step4.png',
    description: 'Click on the subject'
    },
    {
      number: 5,
 image: 'assets/images/step5.png',
    description: 'A question will have four options'
    },
    {
      number: 6,
 image: 'assets/images/step6.png',
  description: 'Click right Option.'
    }
  ];
}

