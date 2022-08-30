/****************************************************************
-Creator: Erika Gonzalez 
-Creation date: 2022-08-29
-Project: EERP Project name
-Epic: EP003 
-UH: UH002, UH003
***************************************************************/


namespace net_6_test_j_ibadev_egon_pr.Models
{
    public partial class SurveyType
    {
        public SurveyType()
        {
            Surveys = new HashSet<Survey>();
        }

        public int IdSurveyType { get; set; }
        public string NameTypeSurvey { get; set; } = null!;

        public virtual ICollection<Survey> Surveys { get; set; }
    }
}
