using DevOpsLogger.Api.Models;
using Xunit;

namespace DevOpsLogger.Tests;

public class IncidentTests
{
    [Fact]
    public void NewIncident_HasDefaultStatus_Open()
    {
        var incident = new Incident
        {
            Title = "Test Incident",
            Description = "This is a test"
        };

        Assert.Equal("Open", incident.Status);
    }
}