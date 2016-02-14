using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public sealed class Parameter
    {
        static List<string> _InterestedFields = new List<string>
            {
                "ID","URL","actualCompletionDate","actualCost","actualDurationMinutes","actualExpenseCost",
                "actualLaborCost","actualRevenue","actualStartDate","actualWorkRequired","assignedToID",
                "billingAmount","categoryID","commitDate","condition","constraintDate","costAmount","costType"
                ,"cpi","csi","description","durationMinutes","durationType","durationUnit","eac","enteredByID"
                ,"entryDate","estCompletionDate","estStartDate","groupID","isCritical","milestoneID","name"
                ,"numberOfChildren","numberOpenOpTasks","parentID","parentLag","parentLagType",
                "percentComplete","personal","plannedCompletionDate","plannedCost","plannedDurationMinutes"
                ,"plannedExpenseCost","workRequired","plannedLaborCost","plannedRevenue","plannedStartDate",
                "previousStatus","priority","progressStatus","projectID","referenceNumber","remainingDurationMinutes",
                "revenueType","roleID","spi","status","statusUpdate","submittedByID","taskConstraint","taskNumber",
                "teamID","wbs"
           };


        public static string Parameters
            {
            get { return GetParameters(); }
            }


        public void AddParameter( string parameterName )
            {
            _InterestedFields.Add(parameterName);
            }

        public void RemoveParameter( string parameter )
            {
            if (_InterestedFields.Contains(parameter))
                _InterestedFields.Remove(parameter);
            else
                {
                Console.WriteLine("Parameter {0} doesn't exist", parameter);
                }
            }

        private static string GetParameters()
            {
            StringBuilder sb = new StringBuilder();
            foreach (string interestedField in _InterestedFields)
                {
                sb.Append(interestedField + ",");
                }

            return sb.ToString().TrimEnd(',');
            }
        }
}
