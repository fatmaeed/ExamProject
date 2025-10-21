import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ExamService } from '../../Services/ExamService/exam-service';
import { ActivatedRoute, Router } from '@angular/router';



@Component({
  selector: 'app-exam-form',
  imports: [FormsModule, CommonModule, ReactiveFormsModule],
  templateUrl: './exam-form.html',
  styleUrl: './exam-form.css'
})
export class ExamForm implements OnInit {

  examId: any;
  constructor(private examService: ExamService, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe({
      next: (params) => {
        this.examId = params.get('id');
        if (this.examId) {
          this.examService.gitById(this.examId).subscribe({
            next: (data) => {
              this.getTitle.setValue(data.name);
              const [hours, minutes] = data.duration.split(':').map(Number);
              this.getDuration.setValue({ hours, minutes });
              this.getMinDegree.setValue(data.minDegree.toString());
              this.getMaxDegree.setValue(data.maxDegree.toString());
              this.getStartTime.setValue(new Date(data.startTime).toISOString().slice(0, 16));
            },
            error: (err) => {
              console.log(err);
            }
          });
        }
      }
    });
    this.examForm.get('starttime')?.valueChanges.subscribe(() => {
  this.updateEndTime();
});

this.examForm.get('duration')?.valueChanges.subscribe(() => {
  this.updateEndTime();
});

  }


  examForm = new FormGroup({
    title: new FormControl('', [Validators.required, Validators.minLength(2)]),
    duration: new FormGroup({
      hours: new FormControl(0, [Validators.required,Validators.max(4)]),
      minutes: new FormControl(0, [Validators.required, Validators.min(0), Validators.max(59)])
    }),
    mindegree: new FormControl('', Validators.required),
    maxdegree: new FormControl('', Validators.required),
    starttime: new FormControl('', Validators.required),
    endtime: new FormControl('', Validators.required),
  });

  get getTitle() {
    return this.examForm.controls['title'];
  }
  get getDuration() {
    return this.examForm.controls['duration'] as FormGroup;
  }
  get getHours() {
    return this.getDuration.controls['hours']
  }
  get getMinutes() {
    return this.getDuration.controls['minutes']
  }
  get getMinDegree() {
    return this.examForm.controls['mindegree'];
  }
  get getMaxDegree() {
    return this.examForm.controls['maxdegree'];
  }
  get getStartTime() {
    return this.examForm.controls['starttime'];
  }
  get getEndTime() {
    return this.examForm.controls['endtime'];
  }




  saveExam() {
    if (this.examForm.status == "VALID") {
      if(this.examId == null){
        const duration = this.getDuration.value;
      const durationString = `${duration.hours.toString().padStart(2, '0')}:${duration.minutes.toString().padStart(2, '0')}:00`;
      const examDTO = {
        Name: this.getTitle.value,
        Duration: durationString,
        MinDegree: this.getMinDegree.value,
        MaxDegree: this.getMaxDegree.value,
        StartTime: this.getStartTime.value,
        EndTime: this.getEndTime.value,
        Questions: []
      };
      console.log(examDTO)
      this.examService.create(examDTO).subscribe({
        next: () => {
          this.router.navigate(['/examlist']);

        },
        error:(err)=>{
            console.error("ERROR", err);
    console.log("Error details:", err.error);
        }
      });
      }else{
        const duration = this.getDuration.value;
      const durationString = `${duration.hours.toString().padStart(2, '0')}:${duration.minutes.toString().padStart(2, '0')}:00`;
      const examDTO = {
        Name: this.getTitle.value,
        Duration: durationString,
        MinDegree: this.getMinDegree.value,
        MaxDegree: this.getMaxDegree.value,
        StartTime: this.getStartTime.value,
        EndTime: this.getEndTime.value
      };
      this.examService.update(this.examId,examDTO).subscribe({
        next:()=>{
          this.router.navigate(['/examlist']);
        },
      });
      }
    }else{
      console.log("error");
    }
  }



  updateEndTime() {
  const startStr = this.getStartTime.value;
  const duration = this.getDuration.value;

  if (!startStr || duration.hours == null || duration.minutes == null) return;

  const start = new Date(startStr);

  const end = new Date(start);
  end.setHours(end.getHours() + +duration.hours);
  end.setMinutes(end.getMinutes() + +duration.minutes);

  const endTimeFormatted = end.toISOString().slice(0, 16);

  this.getEndTime.setValue(endTimeFormatted, { emitEvent: false });
}

}
