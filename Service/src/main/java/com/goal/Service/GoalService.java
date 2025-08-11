package com.goal.Service;

import java.util.List;
import java.util.UUID;
import java.util.stream.Collectors;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.goal.DTO.CreateGoalDto;
import com.goal.DTO.GetGoalDto;
import com.goal.Entities.Goal;
import com.goal.Enum.GoalStatus;

import jakarta.transaction.Transactional;

@Service
@Transactional
public class GoalService implements com.goal.IService.IGoalService {

    @Autowired private com.goal.IRepository.IGoalRepository _goalRepository;

    @Override
    public void createGoal(CreateGoalDto dto) {
        Goal goal = new Goal();
        goal.setId(UUID.randomUUID());
        goal.setUserId(dto.getUserId());
        goal.setTitle(dto.getTitle());
        goal.setDescription(dto.getDescription());
        goal.setStartDate(dto.getStartDate());
        goal.setEndDate(dto.getEndDate());
        goal.setStatus(GoalStatus.ACTIVE);
        _goalRepository.save(goal);
    }



    @Override
    public void updateGoal(UUID id, com.goal.DTO.UpdateGoalDto dto) {
        Goal goal = _goalRepository.findById(id)
                .orElseThrow(() -> new RuntimeException("Goal not found"));

        goal.setTitle(dto.getTitle());
        goal.setDescription(dto.getDescription());
        goal.setStartDate(dto.getStartDate());
        goal.setEndDate(dto.getEndDate());
        goal.setStatus(GoalStatus.ACTIVE); 
        _goalRepository.save(goal);
    }

    @Override
    public void deleteGoal(UUID id) {
          if (!_goalRepository.existsById(id)) {
            throw new RuntimeException("Goal not found");
        }
        _goalRepository.deleteById(id);
    }

    @Override
    public com.goal.DTO.GetGoalDto getGoalByUserId(String userId) {
        
        Goal goal = _goalRepository.findByUserId(userId)
            .orElseThrow(() -> new RuntimeException("Goal not found for userId: " + userId));
        // You may want to map Entities.Goal to DTO.GetGoalDto here
        // Example placeholder:
        if (goal != null) {
            com.goal.DTO.GetGoalDto dto = new com.goal.DTO.GetGoalDto();
            dto.setId(goal.getId());
            dto.setUserId(goal.getUserId().toString());
            dto.setTitle(goal.getTitle());
            dto.setDescription(goal.getDescription());
            dto.setStartDate(goal.getStartDate().toString());
            dto.setEndDate(goal.getEndDate().toString());
            dto.setStatus(goal.getStatus().toString());
            return dto;
        }
        return null;
    }
    
    @Override
    public List<GetGoalDto> getAllGoals(String userId) {
    return _goalRepository.findAll()
            .stream()
            .filter(goal -> goal.getUserId().equals(userId))
            .map(this::mapToGetGoalDto)
            .collect(Collectors.toList());
    }

    
    private GetGoalDto mapToGetGoalDto(Goal goal) {
        GetGoalDto dto = new GetGoalDto();
        dto.setId(goal.getId());
        dto.setUserId(goal.getUserId());
        dto.setTitle(goal.getTitle());
        dto.setDescription(goal.getDescription());
        dto.setStartDate(goal.getStartDate().toString());
        dto.setEndDate(goal.getEndDate().toString());
        dto.setStatus(goal.getStatus().toString());
        return dto;
    }
    
}
