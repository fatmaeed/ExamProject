interface IOptionModel {
    option :string ;
    label : string ;
    isCorrect : boolean ;

}
interface IOptionModelForForm {
    option : AnswerOption;
    label : string ;
    isCorrect : boolean ;

}
type AnswerOption = 'option1' | 'option2' | 'option3' | 'option4';
// interface IOptionModelFormControl {
//     option :any ;
//     label : string ;
//     isCorrect : boolean ;
// }