namespace Finance.Application.Helpers;

public static class PostgressErrorConstant
{
    public static string SuccessfulCompletion() => "00000";
    public static string Warning() => "01000";
    public static string DynamicResultSetsReturned() => "0100C";
    public static string ImplicitZeroBitPadding() => "01008";
    public static string NullValueEliminatedInSetFunction() => "01003";
    public static string PrivilegeNotGranted() => "01007";
    public static string PrivilegeNotRevoked() => "01006";
    // public static string StringDataRightTruncation() => "01004";
    public static string DeprecatedFeature() => "01P01";
    public static string NoData() => "02000";
    public static string NoAdditionalDynamicResultSetsReturned() => "02001";
    public static string SqlStatementNotYetComplete() => "03000";
    public static string ConnectionException() => "08000";
    public static string ConnectionDoesNotExist() => "08003";
    public static string ConnectionFailure() => "08006";
    public static string SqlclientUnableToEstablishSqlconnection() => "08001";
    public static string SqlserverRejectedEstablishmentOfSqlconnection() => "08004";
    public static string TransactionResolutionUnknown() => "08007";
    public static string ProtocolViolation() => "08P01";
    public static string TriggeredActionException() => "09000";
    public static string FeatureNotSupported() => "0A000";
    public static string InvalidTransactionInitiation() => "0B000";
    public static string LocatorException() => "0F000";
    public static string InvalidLocatorSpecification() => "0F001";
    public static string InvalidGrantor() => "0L000";
    public static string InvalidGrantOperation() => "0LP01";
    public static string InvalidRoleSpecification() => "0P000";
    public static string DiagnosticsException() => "0Z000";
    public static string StackedDiagnosticsAccessedWithoutActiveHandler() => "0Z002";
    public static string CaseNotFound() => "20000";
    public static string CardinalityViolation() => "21000";
    public static string DataException() => "22000";
    public static string ArraySubscriptError() => "2202E";
    public static string CharacterNotInRepertoire() => "22021";
    public static string DatetimeFieldOverflow() => "22008";
    public static string DivisionByZero() => "22012";
    public static string ErrorInAssignment() => "22005";
    public static string EscapeCharacterConflict() => "2200B";
    public static string IndicatorOverflow() => "22022";
    public static string IntervalFieldOverflow() => "22015";
    public static string InvalidArgumentForLogarithm() => "2201E";
    public static string InvalidArgumentForNtileFunction() => "22014";
    public static string InvalidArgumentForNthValueFunction() => "22016";
    public static string InvalidArgumentForPowerFunction() => "2201F";
    public static string InvalidArgumentForWidthBucketFunction() => "2201G";
    public static string InvalidCharacterValueForCast() => "22018";
    public static string InvalidDatetimeFormat() => "22007";
    public static string InvalidEscapeCharacter() => "22019";
    public static string InvalidEscapeOctet() => "2200D";
    public static string InvalidEscapeSequence() => "22025";
    public static string NonstandardUseOfEscapeCharacter() => "22P06";
    public static string InvalidIndicatorParameterValue() => "22010";
    public static string InvalidParameterValue() => "22023";
    public static string InvalidRegularExpression() => "2201B";
    public static string InvalidRowCountInLimitClause() => "2201W";
    public static string InvalidRowCountInResultOffsetClause() => "2201X";
    public static string InvalidTablesampleArgument() => "2202H";
    public static string InvalidTablesampleRepeat() => "2202G";
    public static string InvalidTimeZoneDisplacementValue() => "22009";
    public static string InvalidUseOfEscapeCharacter() => "2200C";
    public static string MostSpecificTypeMismatch() => "2200G";
    public static string NullValueNotAllowed() => "22004";
    public static string NullValueNoIndicatorParameter() => "22002";
    public static string NumericValueOutOfRange() => "22003";
    public static string StringDataLengthMismatch() => "22026";
    public static string StringDataRightTruncation() => "22001";
    public static string SubstringError() => "22011";
    public static string TrimError() => "22027";
    public static string UnterminatedCString() => "22024";
    public static string ZeroLengthCharacterString() => "2200F";
    public static string FloatingPointException() => "22P01";
    public static string InvalidTextRepresentation() => "22P02";
    public static string InvalidBinaryRepresentation() => "22P03";
    public static string BadCopyFileFormat() => "22P04";
    public static string UntranslatableCharacter() => "22P05";
    public static string NotAnXmlDocument() => "2200L";
    public static string InvalidXmlDocument() => "2200M";
    public static string InvalidXmlContent() => "2200N";
    public static string InvalidXmlComment() => "2200S";
    public static string InvalidXmlProcessingInstruction() => "2200T";
    public static string IntegritraintViolation() => "23000";
    public static string RestrictViolation() => "23001";
    public static string NotNullViolation() => "23502";
    public static string ForeignKeyViolation() => "23503";
    public static string UniqueViolation() => "23505";
    public static string CheckViolation() => "23514";
    public static string ExclusionViolation() => "23P01";
    public static string InvalidCursorState() => "24000";
    public static string InvalidTransactionState() => "25000";
    public static string ActiveSqlTransaction() => "25001";
    public static string BranchTransactionAlreadyActive() => "25002";
    public static string HeldCursorRequiresSameIsolationLevel() => "25008";
    public static string InappropriateAccessModeForBranchTransaction() => "25003";
    public static string InappropriateIsolationLevelForBranchTransaction() => "25004";
    public static string NoActiveSqlTransactionForBranchTransaction() => "25005";
    public static string ReadOnlySqlTransaction() => "25006";
    public static string SchemaAndDataStatementMixingNotSupported() => "25007";
    public static string NoActiveSqlTransaction() => "25P01";
    public static string InFailedSqlTransaction() => "25P02";
    public static string InvalidSqlStatementName() => "26000";
    public static string TriggeredDataChangeViolation() => "27000";
    public static string InvalidAuthorizationSpecification() => "28000";
    public static string InvalidPassword() => "28P01";
    public static string DependentPrivilegeDescriptorsStillExist() => "2B000";
    public static string DependentObjectsStillExist() => "2BP01";
    public static string InvalidTransactionTermination() => "2D000";
    public static string SqlRoutineException() => "2F000";
    public static string FunctionExecutedNoReturnStatement() => "2F005";
    public static string ModifyingSqlDataNotPermitted() => "2F002";
    public static string ProhibitedSqlStatementAttempted() => "2F003";
    public static string ReadingSqlDataNotPermitted() => "2F004";
    public static string InvalidCursorName() => "34000";
    public static string ExternalRoutineException() => "38000";
    public static string ContainingSqlNotPermitted() => "38001";
    // public static string ModifyingSqlDataNotPermitted() => "38002";
    // public static string ProhibitedSqlStatementAttempted() => "38003";
    // public static string ReadingSqlDataNotPermitted() => "38004";
    public static string ExternalRoutineInvocationException() => "39000";
    public static string InvalidSqlstateReturned() => "39001";
    // public static string NullValueNotAllowed() => "39004";
    public static string TriggerProtocolViolated() => "39P01";
    public static string SrfProtocolViolated() => "39P02";
    public static string EventTriggerProtocolViolated() => "39P03";
    public static string SavepointException() => "3B000";
    public static string InvalidSavepointSpecification() => "3B001";
    public static string InvalidCatalogName() => "3D000";
    public static string InvalidSchemaName() => "3F000";
    public static string TransactionRollback() => "40000";
    public static string TransactionIntegritraintViolation() => "40002";
    public static string SerializationFailure() => "40001";
    public static string StatementCompletionUnknown() => "40003";
    public static string DeadlockDetected() => "40P01";
    public static string SyntaxErrorOrAccessRuleViolation() => "42000";
    public static string SyntaxError() => "42601";
    public static string InsufficientPrivilege() => "42501";
    public static string CannotCoerce() => "42846";
    public static string GroupingError() => "42803";
    public static string WindowingError() => "42P20";
    public static string InvalidRecursion() => "42P19";
    public static string InvalidForeignKey() => "42830";
    public static string InvalidName() => "42602";
    public static string NameTooLong() => "42622";
    public static string ReservedName() => "42939";
    public static string DatatypeMismatch() => "42804";
    public static string IndeterminateDatatype() => "42P18";
    public static string CollationMismatch() => "42P21";
    public static string IndeterminateCollation() => "42P22";
    public static string WrongObjectType() => "42809";
    public static string UndefinedColumn() => "42703";
    public static string UndefinedFunction() => "42883";
    public static string UndefinedTable() => "42P01";
    public static string UndefinedParameter() => "42P02";
    public static string UndefinedObject() => "42704";
    public static string DuplicateColumn() => "42701";
    public static string DuplicateCursor() => "42P03";
    public static string DuplicateDatabase() => "42P04";
    public static string DuplicateFunction() => "42723";
    public static string DuplicatePreparedStatement() => "42P05";
    public static string DuplicateSchema() => "42P06";
    public static string DuplicateTable() => "42P07";
    public static string DuplicateAlias() => "42712";
    public static string DuplicateObject() => "42710";
    public static string AmbiguousColumn() => "42702";
    public static string AmbiguousFunction() => "42725";
    public static string AmbiguousParameter() => "42P08";
    public static string AmbiguousAlias() => "42P09";
    public static string InvalidColumnReference() => "42P10";
    public static string InvalidColumnDefinition() => "42611";
    public static string InvalidCursorDefinition() => "42P11";
    public static string InvalidDatabaseDefinition() => "42P12";
    public static string InvalidFunctionDefinition() => "42P13";
    public static string InvalidPreparedStatementDefinition() => "42P14";
    public static string InvalidSchemaDefinition() => "42P15";
    public static string InvalidTableDefinition() => "42P16";
    public static string InvalidObjectDefinition() => "42P17";
    public static string WithCheckOptionViolation() => "44000";
    public static string InsufficientResources() => "53000";
    public static string DiskFull() => "53100";
    public static string OutOfMemory() => "53200";
    public static string TooManyConnections() => "53300";
    public static string ConfigurationLimitExceeded() => "53400";
    public static string ProgramLimitExceeded() => "54000";
    public static string StatementTooComplex() => "54001";
    public static string TooManyColumns() => "54011";
    public static string TooManyArguments() => "54023";
    public static string ObjectNotInPrerequisiteState() => "55000";
    public static string ObjectInUse() => "55006";
    public static string CantChangeRuntimeParam() => "55P02";
    public static string LockNotAvailable() => "55P03";
    public static string OperatorIntervention() => "57000";
    public static string QueryCanceled() => "57014";
    public static string AdminShutdown() => "57P01";
    public static string CrashShutdown() => "57P02";
    public static string CannotConnectNow() => "57P03";
    public static string DatabaseDropped() => "57P04";
    public static string SystemError() => "58000";
    public static string IoError() => "58030";
    public static string UndefinedFile() => "58P01";
    public static string DuplicateFile() => "58P02";
    public static string ConfigFileError() => "F0000";
    public static string LockFileExists() => "F0001";
    public static string FdwError() => "HV000";
    public static string FdwColumnNameNotFound() => "HV005";
    public static string FdwDynamicParameterValueNeeded() => "HV002";
    public static string FdwFunctionSequenceError() => "HV010";
    public static string FdwInconsistentDescriptorInformation() => "HV021";
    public static string FdwInvalidAttributeValue() => "HV024";
    public static string FdwInvalidColumnName() => "HV007";
    public static string FdwInvalidColumnNumber() => "HV008";
    public static string FdwInvalidDataType() => "HV004";
    public static string FdwInvalidDataTypeDescriptors() => "HV006";
    public static string FdwInvalidDescriptorFieldIdentifier() => "HV091";
    public static string FdwInvalidHandle() => "HV00B";
    public static string FdwInvalidOptionIndex() => "HV00C";
    public static string FdwInvalidOptionName() => "HV00D";
    public static string FdwInvalidStringLengthOrBufferLength() => "HV090";
    public static string FdwInvalidStringFormat() => "HV00A";
    public static string FdwInvalidUseOfNullPointer() => "HV009";
    public static string FdwTooManyHandles() => "HV014";
    public static string FdwOutOfMemory() => "HV001";
    public static string FdwNoSchemas() => "HV00P";
    public static string FdwOptionNameNotFound() => "HV00J";
    public static string FdwReplyHandle() => "HV00K";
    public static string FdwSchemaNotFound() => "HV00Q";
    public static string FdwTableNotFound() => "HV00R";
    public static string FdwUnableToCreateExecution() => "HV00L";
    public static string FdwUnableToCreateReply() => "HV00M";
    public static string FdwUnableToEstablishConnection() => "HV00N";
    public static string PlpgsqlError() => "P0000";
    public static string RaiseException() => "P0001";
    public static string NoDataFound() => "P0002";
    public static string TooManyRows() => "P0003";
    public static string AssertFailure() => "P0004";
    public static string InternalError() => "XX000";
    public static string DataCorrupted() => "XX001";
    public static string IndexCorrupted() => "XX002";
}