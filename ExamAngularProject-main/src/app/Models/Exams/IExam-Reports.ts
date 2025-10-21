interface IExamReports  extends IBaseExam{
    examName: string;
    maxMarks: number;
    minMarks: number;
    totalStudents: number;
    passedStudents: number;
    failedStudents: number;
    averageMarks: number;
    duration: string; 
    startDateTime: string; 
    endDateTime: string;
    students : IStudentReport[];
}