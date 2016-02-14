using System.ComponentModel.DataAnnotations;

namespace AtTaskDataModel
{
    public class AtTaskModel
    {

        [Key]
        public int Id { get; set; }   
        public string atTaskID { get; set; }
        public string name { get; set; }
        public string objCode { get; set; }
        public string URL { get; set; }
        public string actualCompletionDate { get; set; }
        public double actualCost { get; set; }
        public int actualDurationMinutes { get; set; }
        public double actualExpenseCost { get; set; }
        public double actualLaborCost { get; set; }
        public double actualRevenue { get; set; }
        public string actualStartDate { get; set; }
        public int actualWorkRequired { get; set; }
        public string assignedToID { get; set; }
        public double billingAmount { get; set; }
        public string categoryID { get; set; }
        public string commitDate { get; set; }
        public int? condition { get; set; }
        public string constraintDate { get; set; }
        public double costAmount { get; set; }
        public string costType { get; set; }
        public double cpi { get; set; }
        public double csi { get; set; }
        public string description { get; set; }
        public int durationMinutes { get; set; }
        public string durationType { get; set; }
        public string durationUnit { get; set; }
        public double eac { get; set; }
        public string enteredByID { get; set; }
        public string entryDate { get; set; }
        public string estCompletionDate { get; set; }
        public string estStartDate { get; set; }
        public string groupID { get; set; }
        public bool isCritical { get; set; }
        public object milestoneID { get; set; }
        public int numberOfChildren { get; set; }
        public int numberOpenOpTasks { get; set; }
        public string parentID { get; set; }
        public double parentLag { get; set; }
        public string parentLagType { get; set; }
        public string percentComplete { get; set; }
        public bool personal { get; set; }
        public string plannedCompletionDate { get; set; }
        public double plannedCost { get; set; }
        public int plannedDurationMinutes { get; set; }
        public double plannedExpenseCost { get; set; }
        public int workRequired { get; set; }
        public double plannedLaborCost { get; set; }
        public double plannedRevenue { get; set; }
        public string plannedStartDate { get; set; }
        public string previousStatus { get; set; }
        public int priority { get; set; }
        public string progressStatus { get; set; }
        public string projectID { get; set; }
        public int referenceNumber { get; set; }
        public int? remainingDurationMinutes { get; set; }
        public string revenueType { get; set; }
        public string roleID { get; set; }
        public double spi { get; set; }
        public string status { get; set; }
        public string statusUpdate { get; set; }
        public object submittedByID { get; set; }
        public string taskConstraint { get; set; }
        public int taskNumber { get; set; }
        public string teamID { get; set; }
        public string wbs { get; set; }
    }
}