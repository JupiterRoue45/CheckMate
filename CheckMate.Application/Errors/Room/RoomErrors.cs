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

        public static Error InvalidRoomType(int roomTypeId) => Error.Validation(
            code: "Room.InvalidRoomType",
            message: $"Room type with ID {roomTypeId} is invalid."
        );

        public static Error InvalidRoomNumber(string roomNumber) => Error.Validation(
            code: "Room.InvalidRoomNumber",
            message: $"Room number '{roomNumber}' is invalid."
        );

        public static Error InexistantRoomType(int id) => Error.Validation(
            code: "Room.RoomTypeId",
            message: $"the room type with the id {id} does not exist."
            );

    }
}
