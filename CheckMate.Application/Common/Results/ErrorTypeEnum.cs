using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Common.Results
{
    public enum ErrorTypeEnum
    {
        FAILURE = 0,
        VALIDATION = 1,
        NOT_FOUND = 2,
        CONFLICT = 3,
        FORBIDDEN = 4,
    }
}
