using CheckMate.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Errors.RoomType
{
    public static class RoomTypeErrors
    {
        public static Error NotFound(int id) => Error.NotFound(
            code: "RoomType.NotFound",
            message: $"Room type with ID {id} was not found."
        );

        public static Error AlreadyExists(string name) => Error.Conflict(
            code: "RoomType.AlreadyExists",
            message: $"Room type with name '{name}' already exists."
        );
    }
}
