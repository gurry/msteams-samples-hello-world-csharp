namespace Tachyon.SDK.Consumer.Models.Send
{
    using System;
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.Enums;
    using Tachyon.SDK.Consumer.Models.Common;

    /// <summary>
    /// Model representing a single Scheduled Instruction
    /// </summary>
    public class ScheduledInstruction
    {
        /// <summary>
        /// Id
        /// </summary>
        public int? DefinitionId { get; set; }
        /// <summary>
        /// Name of instruction definition
        /// </summary>
        public string DefinitionName { get; set; }
        /// <summary>
        /// Expression defining the scope for this instruction
        /// </summary>
        public ExpressionObject Scope { get; set; }
        /// <summary>
        /// Parameters for the instruction
        /// </summary>
        public List<InstructionParameter> Parameters { get; set; }
        /// <summary>
        /// Instruction's time to live. This defines for how long the system will gather responses.
        /// </summary>
        public int InstructionTtlMinutes { get; set; }
        /// <summary>
        /// Response's time to live. This defined for how long the system will keep responses after it has stopped gathering them.
        /// </summary>
        public int ResponseTtlMinutes { get; set; }
        /// <summary>
        /// Id of the parent instruction
        /// </summary>
        public int? ParentInstructionId { get; set; }
        /// <summary>
        /// Flag defining if responses should be stored as aggregated data or raw and only aggregated when reading the responses.
        /// </summary>
        public bool? KeepRaw { get; set; }
        /// <summary>
        /// Flag indicating if responses should be exported automatically upon expiry
        /// </summary>
        public bool Export { get; set; }
        /// <summary>
        /// Path to where the results of this instruction should be exported upon expiry. Only used if Export is set to true
        /// </summary>
        public string ExportLocation { get; set; }
        /// <summary>
        /// Expression defining the results filter
        /// </summary>
        public ExpressionObject ResultsFilter { get; set; }
        /// <summary>
        /// Expression defining the previous results filter
        /// </summary>
        public ExpressionObject PreviousResultsFilter { get; set; }
        /// <summary>
        /// List of device's Fqdns to send this instruction to. 
        /// </summary>
        public List<string> Devices { get; set; }
        /// <summary>
        /// Custom consumer data
        /// </summary>
        public string ConsumerCustomData { get; set; }
        /// <summary>
        /// Flag indicating if the responses should be offloaded
        /// </summary>
        public bool OffloadResponses { get; set; }
        /// <summary>
        /// Field used by consumers to store information about their own internal user who issued instruction. (ie when different than Tachyon principal)
        /// </summary>
        public string RequestedFor { get; set; }
        /// <summary>
        /// Comments for the instruction
        /// </summary>
        public string Comments { get; set; }
        // Properties related to scheduling. For usage, see https://docs.microsoft.com/en-us/sql/relational-databases/system-tables/dbo-sysschedules-transact-sql
        /// <summary>
        /// Status of the job schedule
        /// </summary>
        public bool ScheduleEnabled { get; set; }
        /// <summary>
        /// How frequently a job runs for this schedule. 1 = One time only 4 = Daily 8 = Weekly 16 = Monthly
        /// </summary>
        public int ScheduleFreqType { get; set; }
        /// <summary>
        /// Days that the job is executed. Depends on the value of freq_type. The default value is 0, which indicates that freq_interval is unused. See the table below for the possible values and their effects
        /// </summary>
        public int? ScheduleFreqInterval { get; set; }
        /// <summary>
        /// Units for the freq_subday_interval. The following are the possible values and their descriptions
        /// </summary>
        public int? ScheduleFreqSubdayType { get; set; }
        /// <summary>
        /// Number of freq_subday_type periods to occur between each execution of the job.
        /// </summary>
        public int? ScheduleFreqSubdayInterval { get; set; }
        /// <summary>
        /// When freq_interval occurs in each month, if freq_interval is 32 (monthly relative).
        /// </summary>
        public int? ScheduleFreqRelativeInterval { get; set; }
        /// <summary>
        /// Number of weeks or months between the scheduled execution of a job. freq_recurrence_factor is used only if freq_type is 8, 16, or 32. If this column contains 0, freq_recurrence_factor is unused
        /// </summary>
        public int? ScheduleFreqRecurrenceFactor { get; set; }
        /// <summary>
        /// Date on which execution of a job can begin. The date is formatted as YYYYMMDD. NULL indicates today's date
        /// </summary>
        public DateTime? ScheduleActiveStartDate { get; set; }
        /// <summary>
        /// Date on which execution of a job can stop. The date is formatted YYYYMMDD
        /// </summary>
        public DateTime? ScheduleActiveEndDate { get; set; }
        /// <summary>
        /// Time on any day between active_start_date and active_end_date that job begins executing. Time is formatted HHMMSS, using a 24-hour clock.
        /// </summary>
        public int? ScheduleActiveStartTime { get; set; }
        /// <summary>
        /// Time on any day between active_start_date and active_end_date that job stops executing. Time is formatted HHMMSS, using a 24-hour clock.
        /// </summary>
        public int? ScheduleActiveEndTime { get; set; }
        // The following properties are not used for "Send"
        // public DateTime? ScheduleLastExecuted { get; set; }
        // public DateTime? ScheduleNextExecution { get; set; }
        // public string ScheduleReadableFrequency { get; set; }
    }
}
