/****************************************************************
-Creator: Erika Gonzalez 
-Creation date: 2022-08-29
-Project: EERP Project name
-Epic: EP003 
-UH: UH002, UH003
***************************************************************/

using System;
using System.Collections.Generic;

namespace net_6_test_j_ibadev_egon_pr.Models
{
    public partial class Survey
    {
        public int IdSurvey { get; set; }
        public string Title { get; set; } = null!;
        public int IdSurveyType { get; set; }
        public string Template { get; set; } = null!;
        public bool Disable { get; set; }
        public bool Published { get; set; }

        public virtual SurveyType IdSurveyTypeNavigation { get; set; } = null!;
    }
}
