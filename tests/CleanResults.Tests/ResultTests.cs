﻿using CleanResults.Abstractions;
using CleanResults.Tests.Errors;
using FluentAssertions;

namespace CleanResults.Tests;
public class ResultTests
{
    [Fact]
    public void Result_Ok_ShouldBeSuccessful()
    {
        // Act
        var result = Result.Ok();

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Error.Should().BeNull();
    }

    [Fact]
    public void Result_Fail_ShouldContainError()
    {
        // Arrange
        var error = new Error("Test error");

        // Act
        var result = Result.Fail(error);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }

    [Fact]
    public void Result_HasError_ShouldReturnTrueForMatchingError()
    {
        // Arrange
        var error = new Error("Test error");
        var result = Result.Fail(error);

        // Act
        var hasError = result.HasError<Error>();

        // Assert
        hasError.Should().BeTrue();
    }

    [Fact]
    public void Result_TryGetError_ShouldReturnErrorIfExists()
    {
        // Arrange
        var error = new Error("Test error");
        var result = Result.Fail(error);

        // Act
        var success = result.TryGetError(out var actualError);

        // Assert
        success.Should().BeTrue();
        actualError.Should().Be(error);
    }

    [Fact]
    public void Constructor_ShouldThrowException_WhenErrorIsNull()
    {
        // Act
        var act = () => new Result((IError)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>()
           .WithMessage("Value cannot be null. (Parameter 'error')");
    }

    [Fact]
    public void Constructor_ShouldAssignError_WhenErrorIsStruct()
    {
        // Arrange
        var error = new StructError("Struct Error");

        // Act
        var result = new Result(error);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }

    [Fact]
    public void HasError_ShouldReturnTrue_WhenErrorMatchesType()
    {
        // Arrange
        var error = new Error("Test Error");
        var result = new Result(error);

        // Act
        var hasError = result.HasError<Error>();

        // Assert
        hasError.Should().BeTrue();
    }

    [Fact]
    public void HasError_ShouldReturnFalse_WhenErrorDoesNotMatchType()
    {
        // Arrange
        var error = new Error("Test Error");
        var result = new Result(error);

        // Act
        var hasError = result.HasError<CustomError>();

        // Assert
        hasError.Should().BeFalse();
    }

    [Fact]
    public void TryGetError_ShouldReturnTrue_WhenErrorExists()
    {
        // Arrange
        var error = new Error("Test Error");
        var result = new Result(error);

        // Act
        var success = result.TryGetError(out var retrievedError);

        // Assert
        success.Should().BeTrue();
        retrievedError.Should().Be(error);
    }

    [Fact]
    public void TryGetError_ShouldReturnFalse_WhenErrorDoesNotExist()
    {
        // Arrange
        var result = Result.Ok();

        // Act
        var success = result.TryGetError(out var retrievedError);

        // Assert
        success.Should().BeFalse();
        retrievedError.Should().BeNull();
    }

    [Fact]
    public void TryGetError_Generic_ShouldReturnTrue_WhenErrorMatchesType()
    {
        // Arrange
        var error = new Error("Test Error");
        var result = new Result(error);

        // Act
        var success = result.TryGetError<Error>(out var retrievedError);

        // Assert
        success.Should().BeTrue();
        retrievedError.Should().Be(error);
    }

    [Fact]
    public void TryGetError_Generic_ShouldReturnFalse_WhenErrorDoesNotMatchType()
    {
        // Arrange
        var error = new Error("Test Error");
        var result = new Result(error);

        // Act
        var success = result.TryGetError<CustomError>(out var retrievedError);

        // Assert
        success.Should().BeFalse();
        retrievedError.Should().BeNull();
    }

    [Fact]
    public void ImplicitOperator_ShouldCreateResultFromError()
    {
        // Arrange
        var error = new Error("Test Error");

        // Act
        Result result = error;

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }

    [Fact]
    public void Ok_ShouldCreateSuccessfulResult_WithValue()
    {
        // Arrange
        var value = "Success Value";

        // Act
        var result = Result.Ok(value);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Value.Should().Be(value);
        result.Error.Should().BeNull();
    }

    [Fact]
    public void Ok_ShouldCreateSuccessfulResult_WhenValueIsNull()
    {
        // Act
        var result = Result.Ok<string?>(null);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeNull();
    }


    [Fact]
    public void Ok_ShouldThrowArgumentNullException_WhenAccessValueInFailedResult()
    {
        // Arrange
        var error = new Error("Test Error");

        // Act
        var act = () => Result.Fail<string>(error).Value;

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Fail_ShouldCreateFailedResult_WithError()
    {
        // Arrange
        var error = new Error("Test Error");

        // Act
        var result = Result.Fail<string>(error);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(error);
    }

    [Fact]
    public void Fail_ShouldThrowException_WhenErrorIsNull()
    {
        // Act
        var act = () => Result.Fail<string>(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>()
           .WithMessage("Value cannot be null. (Parameter 'error')");
    }

    [Fact]
    public void FailIfNull_ShouldReturnSameResult_WhenValueIsNotNull()
    {
        // Arrange
        var value = "Test Value";
        var result = Result.Ok(value);
        var error = new Error("Test Error");

        // Act
        var newResult = result.FailIfNull(error);

        // Assert
        newResult.Should().Be(result);
        newResult.IsSuccess.Should().BeTrue();
        newResult.Value.Should().Be(value);
    }

    [Fact]
    public void FailIfNull_ShouldReturnFailedResult_WhenValueIsNull()
    {
        // Arrange
        var error = new Error("Test Error");
        var result = Result.Ok<string?>(null);

        // Act
        var newResult = result.FailIfNull(error);

        // Assert
        newResult.Should().NotBe(result);
        newResult.IsFailure.Should().BeTrue();
        newResult.Error.Should().Be(error);
    }

    [Fact]
    public void FailIfNull_ShouldThrowException_WhenErrorIsNull()
    {
        // Arrange
        var result = Result.Ok<string?>(null);

        // Act
        var act = () => result.FailIfNull(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>()
           .WithMessage("Value cannot be null. (Parameter 'error')");
    }

    [Fact]
    public void TryGetValue_ShouldReturnTrueAndSetValue_WhenResultIsSuccessful()
    {
        // Arrange
        var value = "Test Value";
        var result = Result.Ok(value);

        // Act
        var success = result.TryGetValue(out var retrievedValue);

        // Assert
        success.Should().BeTrue();
        retrievedValue.Should().Be(value);
    }

    [Fact]
    public void TryGetValue_ShouldReturnFalseAndSetNull_WhenResultIsFailed()
    {
        // Arrange
        var error = new Error("Test Error");
        var result = Result.Fail<string>(error);

        // Act
        var success = result.TryGetValue(out var retrievedValue);

        // Assert
        success.Should().BeFalse();
        retrievedValue.Should().BeNull();
    }
}
