﻿namespace BeaverTinder.Application.Dto.ResponsesAbstraction;

public record FailResponse(bool Successful, string Message, int StatusCode);