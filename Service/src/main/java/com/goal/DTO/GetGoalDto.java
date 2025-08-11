package com.goal.DTO;

import java.util.UUID;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class GetGoalDto {
    
    private UUID id;

    private String userId;

    private String title; 

    private String description;

    private String startDate;

    private String endDate;

    private String status; // GoalStatus enum as String
}
