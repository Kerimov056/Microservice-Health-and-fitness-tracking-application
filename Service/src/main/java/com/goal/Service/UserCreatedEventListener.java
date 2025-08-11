package com.goal.Service;

import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.time.LocalDateTime;

import org.springframework.amqp.core.Message;
import org.springframework.amqp.rabbit.annotation.RabbitListener;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.goal.Entities.Goal;
import com.goal.Enum.GoalStatus;
import com.goal.IRepository.IGoalRepository;
import com.goal.config.RabbitConfig;

@Service
public class UserCreatedEventListener {

    @Autowired
    private IGoalRepository goalRepository;

  @RabbitListener(queues = RabbitConfig.USER_CREATED_QUEUE)
public void handleUserCreatedEvent(Message message) throws IOException {
    String json = new String(message.getBody(), StandardCharsets.UTF_8);

    // JSON'u parse etmek için ObjectMapper kullan
    ObjectMapper objectMapper = new ObjectMapper();
    JsonNode root = objectMapper.readTree(json);
    
    // message.userId alanını al
    String userId = root.path("message").path("userId").asText();

    Goal goal = new Goal();
    goal.setUserId(userId);
    goal.setTitle("İlk hedef");
    goal.setDescription("Hoş geldin hedefi");
    goal.setStatus(GoalStatus.ACTIVE);
    goal.setStartDate(LocalDateTime.now());
    goal.setEndDate(LocalDateTime.now().plusDays(30));
    goalRepository.save(goal);
}

// @RabbitListener(queues = RabbitConfig.USER_CREATED_QUEUE)
// public void handleRawMessage(Message message) {
//     String json = new String(message.getBody(), StandardCharsets.UTF_8);
//     System.out.println("Raw JSON message: " + json);
// }
}
