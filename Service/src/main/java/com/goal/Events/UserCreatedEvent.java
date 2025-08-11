package com.goal.Events;

import java.time.LocalDateTime;
import java.util.UUID;

public class UserCreatedEvent {

    private UUID userId;
    private String email;
    private LocalDateTime  createdAt;

    // boş konstruktor
    public UserCreatedEvent() {}

    // getter və setter-lər
    public UUID getUserId() {
        return userId;
    }

    public void setUserId(UUID userId) {
        this.userId = userId;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public LocalDateTime  getCreatedAt() {
        return createdAt;
    }

    public void setCreatedAt(LocalDateTime  createdAt) {
        this.createdAt = createdAt;
    }
}
