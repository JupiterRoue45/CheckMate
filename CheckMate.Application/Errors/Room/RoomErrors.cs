using CheckMate.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Errors.Room
{
    /// <summary>
    /// Represents a collection of error messages related to room operations.
    /// </summary>
    public static class RoomErrors
    {
        public static Error NotFound(int id) => Error.NotFound(
            code: "Room.NotFound",
            message: $"Room with ID {id} was not found."
        );

        public static Error AlreadyExists(string number) => Error.Conflict(
            code: "Room.AlreadyExists",
            message: $"Room with number '{number}' already exists."
        );

        public static Error InexistantRoomNumber(string roomNumber) => Error.Validation(
            code: "Room.InexistantRoomNumber",
            message: $"Room with number '{roomNumber}' was not found."
        );

    }
}
