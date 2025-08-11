package com.goal.DTO;

import java.time.LocalDateTime;

import jakarta.validation.constraints.Future;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Size;

public class CreateGoalDto {

    @NotNull(message = "User ID is required!")
    private String userId;

    @NotNull(message = "Title is required!")
    @Size(min = 3, max = 100, message = "Title must be between 3 and 100 characters!")
    private String title;

    @Size(max = 500, message = "Description can be up to 500 characters!")
    private String description;

    @NotNull(message = "Start Date is required!")
    private LocalDateTime startDate;

    @NotNull(message = "End Date is required!")
    @Future(message = "End Date must be in the future!")
    private LocalDateTime endDate;

    // Getters & Setters
    public String getUserId() { return userId; }
    public void setUserId(String userId) { this.userId = userId; }

    public String getTitle() { return title; }
    public void setTitle(String title) { this.title = title; }

    public String getDescription() { return description; }
    public void setDescription(String description) { this.description = description; }

    public LocalDateTime getStartDate() { return startDate; }
    public void setStartDate(LocalDateTime startDate) { this.startDate = startDate; }

    public LocalDateTime getEndDate() { return endDate; }
    public void setEndDate(LocalDateTime endDate) { this.endDate = endDate; }
}
