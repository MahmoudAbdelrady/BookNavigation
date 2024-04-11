package com.booknav.springbackend.services;

import com.booknav.springbackend.dto.User.LoggedUserDTO;
import com.booknav.springbackend.dto.User.LoginDTO;
import com.booknav.springbackend.entities.User;
import com.booknav.springbackend.utils.SecurityConstants;
import com.booknav.springbackend.utils.response.Response;
import io.jsonwebtoken.Jwts;
import io.jsonwebtoken.security.Keys;
import org.modelmapper.ModelMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.AuthenticationException;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.stereotype.Service;

import java.nio.charset.StandardCharsets;
import java.util.Date;

@Service
public class AuthService {
    private final AuthenticationManager authManager;

    private final ModelMapper modelMapper;

    @Autowired
    public AuthService(AuthenticationManager authManager, ModelMapper modelMapper) {
        this.authManager = authManager;
        this.modelMapper = modelMapper;
    }

    public ResponseEntity<Object> loginUser(LoginDTO loginDTO) throws Exception {
        try {
            UsernamePasswordAuthenticationToken authToken = new UsernamePasswordAuthenticationToken(loginDTO.getUsernameOrEmail(), loginDTO.getPassword());
            Authentication authentication = authManager.authenticate(authToken);
            User user = (User) authentication.getPrincipal();
            LoggedUserDTO loggedUserDTO = modelMapper.map(user, LoggedUserDTO.class);
            String token = generateJWTToken(loggedUserDTO.getUsername(), user.getUserType());
            loggedUserDTO.setToken(token);
            SecurityContextHolder.getContext().setAuthentication(authentication);
            return ResponseEntity.ok(Response.successRes("Logged in successfully", loggedUserDTO));
        } catch (AuthenticationException e) {
            return ResponseEntity
                    .status(HttpStatus.UNAUTHORIZED)
                    .body(Response.errorRes("Invalid credentials", null));
        } catch (Exception e) {
            throw new Exception("Failed to log in");
        }
    }

    private String generateJWTToken(String username, String userType) {
        return Jwts.builder()
                .issuer("BookNav")
                .subject("Token")
                .claim("username", username)
                .claim("type", userType)
                .signWith(Keys.hmacShaKeyFor(SecurityConstants.JWT_KEY.getBytes(StandardCharsets.UTF_8)))
                .issuedAt(new Date())
                .expiration(new Date(new Date().getTime() + 10000000))
                .compact();
    }
}
